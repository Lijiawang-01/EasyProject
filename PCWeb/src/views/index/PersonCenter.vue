<template>
	<el-row>
		<el-col>
			<el-form :model="form" label-width="120px">
				<el-form-item label="昵称">
					<el-input v-model="form.name" />
				</el-form-item>
				<el-form-item label="密码">
					<el-input v-model="form.password" />
				</el-form-item>
				<el-form-item>
					<el-button type="primary" @click="onSubmit">修改</el-button>
					<el-button>取消</el-button>
				</el-form-item>
			</el-form>
		</el-col>
	</el-row>
</template>

<script lang="ts" setup>
import { reactive } from 'vue';
import store from '@/store';
import { editNickNameOrPassword } from '@/api/user/userApi';
import { ElMessage } from 'element-plus';
import Tool from '@/global';
import { useRouter } from 'vue-router';
const router = useRouter();
const form = reactive({
	name: store().$state.NickName,
	password: '',
});

const onSubmit = async () => {
	let res: boolean = (await editNickNameOrPassword(form.name, form.password)) as any as boolean;
	if (res) {
		ElMessage.success('设置成功！即将退出，请重新登录...');
		setTimeout(function () {
			new Tool().ClearLocalStorage();
			router.push({ path: '/login' });
		}, 2000);
	} else {
		ElMessage.success('设置失败！请联系系统管理员');
	}
};
</script>
