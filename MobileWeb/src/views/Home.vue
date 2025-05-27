<template>
	<router-view></router-view>
	<div style="width: 100vw; height: 40px"></div>
	<van-tabbar v-model="active" active-color="#ee0a24" route @change="onChangeTab" safe-area-inset-bottom>
		<van-tabbar-item replace :to="item.path" v-for="(item, index) in tabbarList" :key="index" :badge="item.badge" :name="item.path">
			<template #icon="props">
				<img :src="props.active ? item.active : item.inactive" style="width: 22px; height: 22px;" />
			</template>
			{{ item.title }}
		</van-tabbar-item>
	</van-tabbar>
</template>
<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue';
import { useI18n } from 'vue-i18n'; // 多语言

const { t } = useI18n(); // t方法取出，t('code')使用
onMounted(() => {});

// Define tabbarList before active
const tabbarList = reactive([
	{
		title: '首页',
		path: '/index',
		inactive: 'https://tidyimages.oss-cn-hangzhou.aliyuncs.com/newh5/home.png',
		active: 'https://tidyimages.oss-cn-hangzhou.aliyuncs.com/newh5/home_active.png',
	},
	{
		title: '分类',
		path: '/classify',
		inactive: 'https://tidyimages.oss-cn-hangzhou.aliyuncs.com/newh5/wash.png', // Original icon for '洗衣'
		active: 'https://tidyimages.oss-cn-hangzhou.aliyuncs.com/newh5/wash_active.png', // Original icon for '洗衣'
	},
	{
		title: '购物车',
		path: '/cart',
		inactive: 'https://tidyimages.oss-cn-hangzhou.aliyuncs.com/newh5/order.png', // Original icon for '订单'
		active: 'https://tidyimages.oss-cn-hangzhou.aliyuncs.com/newh5/order_active.png',  // Original icon for '订单'
		badge: '0',
	},
	{
		title: '我的',
		path: '/my',
		inactive: 'https://tidyimages.oss-cn-hangzhou.aliyuncs.com/newh5/my.png',
		active: 'https://tidyimages.oss-cn-hangzhou.aliyuncs.com/newh5/my_active.png',
	},
]);

const active = ref(tabbarList[0].path);

const onChangeTab = (index: number | string) => {
	console.log(index);
};
</script>

<style scoped lang="less"></style>
