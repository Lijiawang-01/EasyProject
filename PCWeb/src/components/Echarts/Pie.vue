<template>
	<div id="echarsMain-Pie"></div>
</template>
<script lang="ts" setup>
import * as echarts from 'echarts';
//页面加载完成后加载我们的图标
import { onMounted } from 'vue';
onMounted(async () => {
	var chartDom = document.getElementById('echarsMain-Pie');
	var myChart = echarts.init(chartDom as HTMLElement);
	myChart.resize({
		width: 800,
		height: 400,
	});
	//属性 option
	const option = {
		title: {
			text: 'Referer of a Website',
			subtext: 'Fake Data',
			left: 'center',
		},
		tooltip: {
			trigger: 'item',
		},
		legend: {
			orient: 'vertical',
			left: 'left',
		},
		series: [
			{
				name: 'Access From',
				type: 'pie',
				radius: '50%',
				data: [
					{ value: 1048, name: 'Search Engine' },
					{ value: 735, name: 'Direct' },
					{ value: 580, name: 'Email' },
					{ value: 484, name: 'Union Ads' },
					{ value: 300, name: 'Video Ads' },
				],
				emphasis: {
					itemStyle: {
						shadowBlur: 10,
						shadowOffsetX: 0,
						shadowColor: 'rgba(0, 0, 0, 0.5)',
					},
				},
			},
		],
	};
	//绘制图标
	myChart.setOption(option);
});
</script>
