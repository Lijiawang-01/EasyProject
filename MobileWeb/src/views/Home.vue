<template>
	<router-view></router-view>
	<div style="width: 100vw; height: 40px"></div>
	<van-tabbar v-model="active" active-color="#26D7CC" route @change="onChangeTab" safe-area-inset-bottom>
		<van-tabbar-item replace :to="item.path" v-for="(item, index) in tabbarList" :key="index">
			<span :class="active === index ? active : ''"><van-icon name="wap-home-o" />{{ item.title }}</span>
		</van-tabbar-item>
	</van-tabbar>
</template>
<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue';
import { useI18n } from 'vue-i18n'; // 多语言

const { t } = useI18n(); // t方法取出，t('code')使用
onMounted(() => {});

const active = ref(0);

const tabbarList = reactive([
	{
		title: '首页',
		path: '/index',
		inactive: 'https://tidyimages.oss-cn-hangzhou.aliyuncs.com/newh5/home.png',
		active: 'https://tidyimages.oss-cn-hangzhou.aliyuncs.com/newh5/home_active.png',
	},
	{
		title: '洗衣',
		path: '/wash',
		inactive: 'https://tidyimages.oss-cn-hangzhou.aliyuncs.com/newh5/wash.png',
		active: 'https://tidyimages.oss-cn-hangzhou.aliyuncs.com/newh5/wash_active.png',
	},
	{
		title: '订单',
		path: '/order',
		inactive: 'https://tidyimages.oss-cn-hangzhou.aliyuncs.com/newh5/order.png',
		active: 'https://tidyimages.oss-cn-hangzhou.aliyuncs.com/newh5/order_active.png',
	},
	{
		title: '我的',
		path: '/my',
		inactive: 'https://tidyimages.oss-cn-hangzhou.aliyuncs.com/newh5/my.png',
		active: 'https://tidyimages.oss-cn-hangzhou.aliyuncs.com/newh5/my_active.png',
	},
]);
const onChangeTab = (index: number | string) => {
	console.log(index);
};
</script>

<style scoped lang="less"></style>
