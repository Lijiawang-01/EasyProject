<template>
	<van-pull-refresh v-model="loading" @refresh="onRefresh">
		<p>刷新次数: {{ count }}</p>
		<van-steps direction="vertical" :active="0">
			<van-step>
				<h3>【城市】物流状态1</h3>
				<p>2016-07-12 12:40</p>
			</van-step>
			<van-step>
				<h3>【城市】物流状态2</h3>
				<p>2016-07-11 10:00</p>
			</van-step>
			<van-step>
				<h3>快件已发货</h3>
				<p>2016-07-10 09:30</p>
			</van-step>
		</van-steps>
		<van-cell title="显示分享面板" @click="showShare = true" />
		<van-share-sheet v-model:show="showShare" title="立即分享给好友" :options="options" @select="onSelect" />
		<div>state中的数据响应式展示： {{ count }}</div>
		<div>getters中的数据响应式展示： {{ doubleCount }}</div>
		<button @click="addNum(10)">点击操作state中的数据</button>
	</van-pull-refresh>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia';
import { orderStore } from '../store/order'; //引入order模块数据
import { showImagePreview, showToast, ToastOptions, useCurrentLang } from 'vant';
import { ref } from 'vue';

const currentLang = useCurrentLang();
const loading = ref(false);
console.log(currentLang.value); // --> 'zh-CN'
const orderstore = orderStore();
const { count, doubleCount } = storeToRefs(orderstore); //解构成响应式数据
const addNum = (num: number) => {
	orderstore.countAdd(num); //用actions中的方法
};
showImagePreview({
	images: ['https://fastly.jsdelivr.net/npm/@vant/assets/apple-1.jpeg', 'https://fastly.jsdelivr.net/npm/@vant/assets/apple-2.jpeg'],
	closeable: true,
});
const showShare = ref(false);
const options = [
	{ name: '微信', icon: 'wechat' },
	{ name: '微博', icon: 'weibo' },
	{ name: '复制链接', icon: 'link' },
	{ name: '分享海报', icon: 'poster' },
	{ name: '二维码', icon: 'qrcode' },
];

const onSelect = (option: { name: string | ToastOptions | undefined }) => {
	showToast(option.name);
	showShare.value = false;
};
const onRefresh = () => {
	setTimeout(() => {
		showToast('刷新成功');
		loading.value = false;
		count.value++;
	}, 1000);
};
</script>

<style scoped lang="less"></style>
