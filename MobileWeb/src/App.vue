<template>
	<van-config-provider theme="light">
		<van-watermark image="https://fastly.jsdelivr.net/npm/@vant/assets/vant-watermark.png" opacity="0.2" />
		<van-notice-bar scrollable mode="closeable">米袋虽空——樱花开哉！</van-notice-bar>

		<!-- 开启顶部安全区适配 -->
		<!-- <van-nav-bar safe-area-inset-top /> -->
		<router-view />
		<!-- 开启底部安全区适配 -->
	</van-config-provider>
</template>
<script setup lang="ts">
import { showSuccessToast, showFailToast } from 'vant';
</script>
<style scoped lang="less"></style>
