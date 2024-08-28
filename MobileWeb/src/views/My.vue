<template>
	<van-form @submit="onSubmit">
		<van-cell-group inset>
			<van-field v-model="username" name="用户名" label="用户名" placeholder="用户名" :rules="[{ required: true, message: '请填写用户名' }]" />
			<van-field
				v-model="password"
				type="password"
				name="密码"
				label="密码"
				placeholder="密码"
				:rules="[{ required: true, message: '请填写密码' }]"
			/>
			<van-field name="switch" label="开关">
				<template #input>
					<van-switch v-model="checked" />
				</template>
			</van-field>
			<van-field name="checkbox" label="复选框">
				<template #input>
					<van-checkbox v-model="checked" shape="square" />
				</template>
			</van-field>
			<van-field name="checkboxGroup" label="复选框组">
				<template #input>
					<van-checkbox-group v-model="groupChecked" direction="horizontal">
						<van-checkbox name="1" shape="square">复选框 1</van-checkbox>
						<van-checkbox name="2" shape="square">复选框 2</van-checkbox>
					</van-checkbox-group>
				</template>
			</van-field>
			<van-field name="radio" label="单选框">
				<template #input>
					<van-radio-group v-model="radioChecked" direction="horizontal">
						<van-radio name="1">单选框 1</van-radio>
						<van-radio name="2">单选框 2</van-radio>
					</van-radio-group>
				</template>
			</van-field>
			<van-field name="stepper" label="步进器">
				<template #input>
					<van-stepper v-model="stepperValue" />
				</template>
			</van-field>
			<van-field name="rate" label="评分">
				<template #input>
					<van-rate v-model="stepperValue" />
				</template>
			</van-field>
			<van-field name="slider" label="滑块">
				<template #input>
					<van-slider v-model="stepperValue" />
				</template>
			</van-field>
			<van-field name="uploader" label="文件上传">
				<template #input>
					<van-uploader v-model="fileValue" />
				</template>
			</van-field>
			<van-field v-model="result" is-link readonly name="picker" label="选择器" placeholder="点击选择城市" @click="showPicker = true" />
			<van-popup v-model:show="showPicker" position="bottom">
				<van-picker :columns="columns" @confirm="onConfirm" @cancel="showPicker = false" />
			</van-popup>
			<van-field v-model="result2" is-link readonly name="datePicker" label="时间选择" placeholder="点击选择时间" @click="showPicker2 = true" />
			<van-popup v-model:show="showPicker2" position="bottom">
				<van-date-picker @confirm="onConfirm2" @cancel="showPicker2 = false" />
			</van-popup>
			<van-field v-model="areaCode" is-link readonly name="area" label="地区选择" placeholder="点击选择省市区" @click="showArea = true" />
			<van-popup v-model:show="showArea" position="bottom">
				<van-area :area-list="areaList" @confirm="onConfirm3" @cancel="showArea = false" />
			</van-popup>
		</van-cell-group>
		<div style="margin: 16px">
			<van-button round block type="primary" native-type="submit"> 提交 </van-button>
		</div>
	</van-form>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { areaList } from '@vant/area-data';

const checked = ref(false);
const groupChecked = ref([]);
const radioChecked = ref('1');
const stepperValue = ref(1);
const fileValue = ref([{ url: 'https://fastly.jsdelivr.net/npm/@vant/assets/leaf.jpeg' }]);
const username = ref('');
const password = ref('');
const onSubmit = (values: any) => {
	console.log('submit', values);
};
const result = ref('');
const showPicker = ref(false);
const columns = [
	{ text: '杭州', value: 'Hangzhou' },
	{ text: '宁波', value: 'Ningbo' },
	{ text: '温州', value: 'Wenzhou' },
	{ text: '绍兴', value: 'Shaoxing' },
	{ text: '湖州', value: 'Huzhou' },
];

const onConfirm = ({ selectedOptions }: any) => {
	result.value = selectedOptions[0]?.text;
	showPicker.value = false;
};

const result2 = ref('');
const showPicker2 = ref(false);
const onConfirm2 = ({ selectedOptions }: any) => {
	result2.value = selectedOptions[0]?.text;
	showPicker2.value = false;
};
const result3 = ref('');
const areaCode = ref('');
const showArea = ref(false);
const onConfirm3 = ({ selectedOptions }: any) => {
	showArea.value = false;
	areaCode.value = selectedOptions.map((item: any) => item.text).join('/');
};
</script>
