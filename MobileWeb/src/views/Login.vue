<template>
	<!-- 导航栏 -->
	<van-nav-bar class="page-nav-bar" title="登录" />
	<!-- /导航栏 -->
	<div class="login-container">
		<!-- 登录表单 -->
		<van-form @submit="onSubmit" >
			<van-field v-model="form.userName" name="userName" label="用户名" placeholder="用户名" :rules="[{ required: true, message: '请填写用户名' }]" />
			<van-field
				v-model="form.passWord"
				type="password"
				name="passWord"
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
import { ref ,reactive} from 'vue';
import { useI18n } from 'vue-i18n'; // 多语言
import { getToken } from '@/api/login/loginApi';

const { t } = useI18n(); // t方法取出，t('code')使用
const username = ref('');
const password = ref('');
const form =  reactive({
	userName: '',
	passWord: '',
});

const onSubmit = (values: any) => {
	getToken(form.userName, form.passWord)
				.then(async (res) => {
					const token = res as any as string;
					localStorage['token'] = token;
					console.log(token);
				})
				.catch((err) => {
					console.log(err);
				});
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
