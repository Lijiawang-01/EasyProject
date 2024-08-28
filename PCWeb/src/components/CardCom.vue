<template>
	<div class="card">
		<div class="left">
			<coffee-cup v-if="info?.icon == 'CoffeeCup'" />
			<apple v-if="info?.icon == 'Apple'" />
			<drizzling v-if="info?.icon == 'Drizzling'" />
			<headset v-if="info?.icon == 'Headset'" />
		</div>
		<div class="right">
			<div>
				<span>{{ info?.title }}</span>
			</div>
			<div>
				<span>{{ info?.count }}</span>
			</div>
		</div>
	</div>
</template>
<script lang="ts" setup>
import { CoffeeCup, Apple, Drizzling, Headset } from '@element-plus/icons-vue';
import { CardModel } from '../class/CardModel';
defineProps({
	info: CardModel,
});
</script>
<style lang="scss" scoped>
.card {
	height: 50px;
	line-height: 25px;
}

.left {
	width: 50px;
	height: 50px;
	float: left;

	.el-icon {
		font-size: 50px;
	}
}

.right {
	width: 100px;
	height: 50px;
	float: right;
}
</style>
