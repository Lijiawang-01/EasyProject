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
		<!-- <BorderBox1 class="container"> Content </BorderBox1> -->
		<div>
			<el-input v-model="userid" :disabled="isdisabled" placeholder="输入账号" />
			<el-input v-model="password" :disabled="isdisabled" placeholder="输入密码" />
			<el-button type="primary" :disabled="isdisabled" @click="Login">登录</el-button>
			<div>
				<ul>
					<li v-for="user in LoginUser" :key="user">{{ user }}用户登录</li>
				</ul>
			</div>
			<el-input v-model="sendUserid" placeholder="发送给某人" />
			<el-input v-model="sendContent" placeholder="发送内容" />
			<el-button type="primary" @click="SendAll">发送所有人</el-button>
			<div>
				<ul>
					<li v-for="user in msgContent" :key="user">{{ user }}</li>
				</ul>
			</div>
		</div>
	</div>
</template>
<script lang="ts" setup>
// eslint-disable-next-line no-unused-vars, @typescript-eslint/no-unused-vars
import { ref, reactive } from 'vue';
import CardCom from '@/components/CardCom.vue';
import Pie from '@/components/Echarts/Pie.vue';
import Histogram from '@/components/Echarts/Histogram.vue';
import Line from '@/components/Echarts/Line.vue';
import { CardModel } from '@/class/CardModel';
// import { BorderBox1 } from '@dataview/datav-vue3';
import { HubConnection } from '@microsoft/signalr';
import * as signalR from '@microsoft/signalr';

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

var connection: HubConnection;
const LoginUser = ref([]);

const msgContent = reactive([] as string[]);

const isdisabled = ref(false);
const userid = ref('');
const password = ref('');
const sendUserid = ref('');
const sendContent = ref('');
const Login = async () => {
	connection = new signalR.HubConnectionBuilder()
		.withUrl('/ChatHubApi?userName='+userid.value) //这里一定要写指定的ip，否则报错，大坑搞了1天的时间
		.withAutomaticReconnect()
		.build();
	await connection.start();
	connection.on('ReceiveMessage', (user, msg) => {
		//监听发送的信息，前后端要一致
		msgContent.push(user + '说：' + msg);
	});

	connection.on('ReceiveBiddingServerTime', (user) => {
		//监听发送的信息，前后端要一致
		//msgContent.push(user);
	});
	isdisabled.value = true;
};
const SendAll = async () => {
	await connection.invoke('SendAllMessage', userid.value, sendContent.value); //发送消息
};
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
}
</style>
