#include <Windows.h>
#include "Functions.h"
#include "Main.h"
#include <math.h>

static DWORD cshellAddress = 0x0;
static DWORD baseAddress = (DWORD)0x00400000;
static float aspectRatio = 1.337f;
static DWORD resolutionX = 1024;
static DWORD resolutionY = 768;
static DWORD verticalRadians = 0;
static DWORD horizontalRadians = 0;


bool Hook(void * toHook, void * ourFunction, int lenght)
{
	if (lenght < 5)
		return false;

	DWORD curProtectionFlag;
	VirtualProtect(toHook, lenght, PAGE_EXECUTE_READWRITE, &curProtectionFlag);
	memset(toHook, 0x90, lenght);
	DWORD relativeAddress = ((DWORD)ourFunction - (DWORD)toHook) - 5;

	*(BYTE*)toHook = 0xE9;
	*(DWORD*)((DWORD)toHook + 1) = relativeAddress;
	
	DWORD temp;
	VirtualProtect(toHook, lenght, curProtectionFlag, &temp);
	return true;
}

DWORD jmpResAddress;
DWORD jmpFovAddress;
float horizontalRadiansF = 1.57f;
float ViewmodelFOVF;
DWORD jmpViewModelRet;
bool d3drenHooked = false;

static float modifyViewmodels(float val)
{
	float tempVradian = 2 * atanf(tanf(val / 2.0f) * ((float)resolutionY/resolutionX));
	return (2.0f * atanf(tanf(tempVradian / 2.0f) * 1.333333f));
}

static float increaseHorFOV()
{
	float tempVradian = 2 * atanf(tanf(horizontalRadiansF / 2.0f) * 0.75f);
	return (2.0f * atanf(tanf(tempVradian / 2.0f) * aspectRatio));
}

void __declspec(naked) ViewModelHack()
{
	/*
	"d3d.ren"+107F2:
	fstp dword ptr [ebx+4C]
	fld dword ptr [ebx+00000098]
	*/
	__asm
	{
		fstp dword ptr[ViewmodelFOVF]
		push eax
		push ebx
		push ecx
		push edx
		push esi
		push edi
		push esp
		push ebp
	}

	ViewmodelFOVF = modifyViewmodels(ViewmodelFOVF);
	ViewmodelFOVF = modifyViewmodels(ViewmodelFOVF);

	__asm
	{
		pop ebp
		pop esp
		pop edi
		pop esi
		pop edx
		pop ecx
		pop ebx
		pop eax
		fld dword ptr[ViewmodelFOVF]
		fstp dword ptr[ebx + 0x4C]
		fld dword ptr[ebx + 0x00000098]
		jmp[jmpViewModelRet]
	}
}

void __declspec(naked) fovHack()
{
	__asm
	{
		fstp dword ptr[ecx + 0x000001C4]
		push ebx
		push ecx
		push edx
		push esi
		push edi
		push esp
		push ebp
		mov horizontalRadiansF, eax
	}
	horizontalRadiansF = increaseHorFOV();

	__asm
	{
		pop ebp
		pop esp
		pop edi
		pop esi
		pop edx
		pop ecx
		pop ebx
		mov eax, horizontalRadiansF
		mov[ecx + 0x000001C0], eax
		jmp[jmpFovAddress]
	}
}

void __declspec(naked) resHack()
{
	__asm
	{
		mov eax, [resolutionX]
		mov ecx, [resolutionY]
		jmp[jmpResAddress]
	}
}

DWORD WINAPI HookThread(LPVOID param)
{
	MODULEINFO cshell = GetModuleInfo("cshell.dll");
	cshellAddress = (DWORD)cshell.lpBaseOfDll;

	//Resolution Hooking
	{
		//Overriding:
		//mov eax,[edx]       - 2 OP bytes
		//mov ecx,[edx + 04]  - 3 OP bytes
		int hookLenght = 0x5;
		DWORD hookAddress = cshellAddress+0xEF79; 		//Solve address = "cshell.dll"+EF79
		jmpResAddress = hookAddress + hookLenght;
		Hook((void*)hookAddress, resHack, hookLenght);
	}

	//FOV hack
	{
		//Overriding:
		//fstp dword ptr [ecx+000001C4]	- 6 OP bytes
		//mov [ecx+000001C0],eax		- 6 OP bytes
		int hookLenght = 12;
		DWORD hookAddress = baseAddress + 0xC370; 		//Solve address = "lithtech.exe"+C370
		jmpFovAddress = hookAddress + hookLenght;
		Hook((void*)hookAddress, fovHack, hookLenght);
	}

	while (true)
	{
		aspectRatio = (float)(1.0 * resolutionX / resolutionY);
		MODULEINFO d3dren = GetModuleInfo("d3d.ren");
		if (d3dren.lpBaseOfDll == 0x0)
		{
			d3drenHooked = false;
		}
		else if(!d3drenHooked)
		{
			#ifdef _DEBUG
			OutputDebugString("d3dRenHooked");
			#endif // DEBUG

			//ViewModel hack
			//Overriding:
			//fstp dword ptr [ebx+4C]
			//fld dword ptr[ebx + 00000098]
			int hookLenght = 9;
			DWORD hookAddress = (DWORD)d3dren.lpBaseOfDll + 0x107F2; 		//Solve address = "cshell.dll"+64A0B
			jmpViewModelRet = hookAddress + hookLenght;
			Hook((void*)hookAddress, ViewModelHack, hookLenght);
			d3drenHooked = true;
		}
		Sleep(400);
	}
}

BOOL WINAPI DllMain(HINSTANCE hModule, DWORD dwReason, LPVOID lpReserved)
{
	//starts from here
	switch (dwReason)
	{
		case DLL_PROCESS_ATTACH:
			CreateThread(0, 0, HookThread, hModule, 0, 0);
			break;
	}

	return true;
}