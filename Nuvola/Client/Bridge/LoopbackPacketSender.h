#ifndef GUARD_LoopbackPacketSender
#define GUARD_LoopbackPacketSender
#include "../../Utils/Math.h"
#include "../../Utils/Utils.h"
#include "../../Mem/Mem.h"
#include "../../Utils/Nuv.h"
struct LoopbackPacketSender {
	/* Fields */
	/* Virtuals */
	virtual void virt_pad_0() {};
	virtual void virt_pad_1() {};
	virtual auto sendToServer(void* packet) -> void {};
	/* Functions */
};
#endif