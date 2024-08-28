<template>
	<van-tag round type="primary">标签</van-tag>

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
		<van-field v-model="tel" type="tel" label="手机号" />
		<!-- 允许输入正整数，调起纯数字键盘 -->
		<van-field v-model="digit" type="digit" label="整数" />
		<!-- 允许输入数字，调起带符号的纯数字键盘 -->
		<van-field v-model="number" type="number" label="数字" />
		<span>选择单个日期</span>
		<van-cell title="选择单个日期" :value="date" @click="show = true" />
		<van-calendar v-model:show="show" @confirm="onConfirm" />
		<span>级联选择</span>
		<van-field v-model="fieldValue" is-link readonly label="地区" placeholder="请选择所在地区" @click="show = true" />
		<van-popup v-model:show="show" round position="bottom">
			<van-cascader v-model="cascaderValue" title="请选择所在地区" :options="options" @close="show = false" @finish="onFinish" />
		</van-popup>
		<span>选框</span>
		<van-checkbox v-model="checked">复选框</van-checkbox>
		<span>多选框</span>
		<van-checkbox-group v-model="checked2">
			<van-checkbox name="a">复选框 a</van-checkbox>
			<van-checkbox name="b">复选框 b</van-checkbox>
		</van-checkbox-group>
		<span>日期选择</span>
		<span>底部提交</span>
		<!-- <van-submit-bar :price="3050" button-text="提交订单" @submit="onSubmit">
			<van-checkbox v-model="checked">全选</van-checkbox>
			<template #tip> 你的收货地址不支持配送, <span @click="onClickLink">修改地址</span> </template>
		</van-submit-bar> -->
	</van-form>
</template>

<script setup lang="ts">
import { showToast } from 'vant/lib/toast';
import { ref, onMounted } from 'vue';
onMounted(() => {});
const onSubmit = () => showToast('点击按钮');
const onClickLink = () => showToast('修改地址');
const checked = ref(false);
const username = ref('');
const password = ref('');
const date = ref('');
const checked2 = ref(['a', 'b']);
const formatDate = (date: { getMonth: () => number; getDate: () => any }) => `${date.getMonth() + 1}/${date.getDate()}`;
const onConfirm = (value: any) => {
	show.value = false;
	console.log(value);
	date.value = formatDate(value);
};
const show = ref(false);
const fieldValue = ref('');
const cascaderValue = ref('');
// 选项列表，children 代表子选项，支持多级嵌套
const options = [
	{
		text: '浙江省',
		value: '330000',
		children: [{ text: '杭州市', value: '330100' }],
	},
	{
		text: '江苏省',
		value: '320000',
		children: [{ text: '南京市', value: '320100' }],
	},
];
// 全部选项选择完毕后，会触发 finish 事件
const onFinish = ({ selectedOptions }: any) => {
	show.value = false;
	fieldValue.value = selectedOptions.map((option: { text: any }) => option.text).join('/');
};
const tel = ref('');
const digit = ref('');
const number = ref('');
</script>
<style scoped lang="less"></style>
