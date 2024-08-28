<template>
	<div class="cardContent">
		<el-card class="box-card" v-for="(item, index) in list" :key="index">
			<CardCom :info="item"> </CardCom>
		</el-card>
		<el-card class="left">
			<!-- 饼图 -->
			<Pie></Pie>
		</el-card>
		<el-card class="right">
			<!-- 柱状图 -->
			<Histogram></Histogram>
		</el-card>
		<el-card class="lineCard">
			<!-- 折线图 -->
			<Line></Line>
		</el-card>
	</div>
	<OfficeDocx></OfficeDocx>
	<OfficePdf></OfficePdf>
	<OfficeExcel></OfficeExcel>
	<!-- 预览video -->
	<!-- <div style="height: 600px">
		<Video :src="videosrc"></Video>
	</div> -->
	<!-- 富文本编辑器 -->
	<!-- <editor v-model="value" style="height: 600px" /> -->
</template>
<script lang="ts" setup>
import { ref, reactive } from 'vue';
import CardCom from '@/components/CardCom.vue';
import Pie from '@/components/Echarts/Pie.vue';
import Histogram from '@/components/Echarts/Histogram.vue';
import Line from '@/components/Echarts/Line.vue';
import { CardModel } from '@/class/CardModel';
//富文本编辑器
//import Editor from '@/components/WangEditor/index.vue';
// 引入axios用来发请求
import axios from 'axios';
//video
import Video from '@/components/Preview/Video.vue';
import OfficeDocx from '@/components/Preview/OfficeDocx.vue';
import OfficePdf from '@/components/Preview/OfficePdf.vue';
import OfficeExcel from '@/components/Preview/OfficeExcel.vue';

const value = ref('初始内容');
const videosrc = ref('https://media.w3.org/2010/05/sintel/trailer.mp4');
const officeUrl = ref('http://localhost:8002/assets/upload/20230711-001采购供应商付款条件审批需求确认单.doc');
let childRef = document.getElementsByClassName('childRef');
let list = [
	{
		title: '新增用户',
		icon: 'CoffeeCup',
		count: 10291,
	},
	{
		title: '未读消息',
		icon: 'Apple',
		count: 8912,
	},
	{
		title: '成交金额',
		icon: 'Drizzling',
		count: 9280,
	},
	{
		title: '购物总量',
		icon: 'Headset',
		count: 13600,
	},
] as CardModel[];
let videoOption = reactive({
	autoplay: true,
	controls: true,
	sources: [
		{
			src: 'https://playtv-live.ifeng.com/live/06OLEGEGM4G.m3u8',
			type: 'video/m3u8',
		},
	],
	width: '640px',
	height: '360px',
});
</script>
<style lang="scss" scoped>
.cardContent {
	width: 100%;
	margin: 0px auto;

	.box-card {
		float: left;
		width: 24%;
		margin-right: 5px;
		margin-bottom: 20px;
	}

	.left,
	.right {
		float: left;
		width: 48%;
		margin-bottom: 20px;
	}

	.lineCard {
		width: 97.5%;
	}

	.right {
		margin-left: 20px;
	}
	.container {
		width: 500px;
		height: 200px;
	}
	.docWrap {
		// 指定样式宽度
		width: 900px;
		height: 100px;
		overflow-x: auto;
	}
}
</style>
