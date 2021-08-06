#ifndef GUARD_MinecraftUIRenderContext
#define GUARD_MinecraftUIRenderContext
#include "../../Utils/Math.h"
#include "../../Utils/Utils.h"
#include "../../Mem/Mem.h"
struct MinecraftUIRenderContext {
	/* Fields */
	class ClientInstance* clientInstance;
	class ScreenContext* screenContext;
	char padding_40[16];
	float textAlpha;
	char padding_240[196];
	class UIScene* uiScene;
	/* Virtuals */
	virtual void virt_pad_0() {};
	virtual void virt_pad_1() {};
	virtual void virt_pad_2() {};
	virtual void virt_pad_3() {};
	virtual void virt_pad_4() {};
	virtual void virt_pad_5() {};
	virtual auto flushText(float param_1) -> void {};
	virtual void virt_pad_7() {};
	virtual void virt_pad_8() {};
	virtual void virt_pad_9() {};
	virtual void virt_pad_10() {};
	virtual void virt_pad_11() {};
	virtual void virt_pad_12() {};
	virtual auto fillRectangle(float* rect, float* color, float alpha) -> void {};
	/* Functions */
};
#endif