<template>
	<!-- 导航栏 -->
	<van-nav-bar class="page-nav-bar" title="登录" />
	<!-- /导航栏 -->
	<div class="login-container">
		<!-- 登录表单 -->
		<van-form @submit="onSubmit" ref="form">
			<van-field v-model="username" name="username" label="用户名" placeholder="用户名" :rules="[{ required: true, message: '请填写用户名' }]" />
			<van-field
				v-model="password"
				type="password"
				name="password"
				label="密码"
				placeholder="密码"
				:rules="[{ required: true, message: '请填写密码' }]"
			/>
			<div class="login-btn-wrap">
				<van-button class="login-btn" block native-type="submit"> 登录 </van-button>
			</div>
		</van-form>
		<!-- /登录表单 -->
	</div>
</template>
<script setup lang="ts">
import router from '@/router';
import { ref } from 'vue';
import { useI18n } from 'vue-i18n'; // 多语言

const { t } = useI18n(); // t方法取出，t('code')使用
const username = ref('');
const password = ref('');
const form = ref();

const onSubmit = (values: any) => {
	console.log('submit', values);
	router.push('/index');
};
</script>
<style scoped lang="less">
.login-container {
	display: flex;
	align-items: center;
	justify-content: center;
	flex-direction: column;
	background: #dbf3d6;
	background-size: cover;
	position: fixed;
	top: 0;
	left: 0;
	width: 100%;
	height: 100%;
	/* background: url(../assets/img/login_bg.png); */
	/* background: url(../assets/img/1.jpg); */
	background-size: 100% 100%;
	background-position: center center;
	overflow: auto;
}
</style>
