#ifndef GUARD_GuiData
#define GUARD_GuiData
#include "../../Utils/Math.h"
#include "../../Utils/Utils.h"
#include "../../Mem/Mem.h"
#include "../../Utils/Nuv.h"
struct GuiData {
	/* Fields */
	char padding_24[24];
	Vector2<float> resolution;
	char padding_40[8];
	Vector2<float> scaledResolution;
	char padding_56[8];
	float scale;
	char padding_82[22];
	Vector2<short> mousePos;
	/* Virtuals */
	/* Functions */
	static inline uintptr_t holder_displayClientMessage;
	auto __thiscall displayClientMessage(nuv::string message) -> void {
		if(holder_displayClientMessage == 0) {
			holder_displayClientMessage = Mem::FindSig("48 89 ?? ?? ?? 48 89 ?? ?? ?? 55 57 41 ?? 41 ?? 41 ?? 48 8D ?? ?? ?? 48 81 EC ?? ?? ?? ?? 48 8B ?? ?? ?? ?? ?? 48 33 ?? 48 89 ?? ?? 4C 8B ?? 48 C7 44 24 58 ?? ?? ?? ?? 45 33 ?? 4C 89 ?? ?? ?? 44 88 ?? ?? ?? 48 C7 44 24 38 ?? ?? ?? ?? 4C 89 ?? ?? ?? 44 88 ?? ?? ?? 48 C7 44 24 78 ?? ?? ?? ?? 4C 89 ?? ?? ?? 44 88 ?? ?? ?? C7 45 80 ?? ?? ?? ?? 0F 10 ?? ?? ?? 0F 11 ?? ?? 4C 89 ?? ?? 48 C7 45 A0 ?? ?? ?? ?? 4C 89 ?? ?? 4C 89 ?? ?? 48 8D ?? ?? E8 ?? ?? ?? ??");
		}
		if(holder_displayClientMessage == 0){
			Utils::DebugF("FATAL: Sig failure for displayClientMessage");
		}
		holder_displayClientMessage += -0;
		return ((void(__thiscall*)(GuiData*, nuv::string message))holder_displayClientMessage)(this, message);
	};
};
#endif