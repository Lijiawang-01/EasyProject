<script setup lang="ts">
// 引入 VueOffice 组件
import VueOfficeDocx from '@vue-office/docx';
// 引入相关样式
import '@vue-office/docx/lib/index.css';
import { ref } from 'vue';

// 设置文档网络地址，可以是本地文件
const src = ref();

// 本地预览
const previewFile = async (event: Event) => {
	const file = (event.target as HTMLInputElement).files?.[0];
	src.value = file;
};

// 请求预览
const getFile = () => {
	src.value = 'https://yjy-teach-oss.oss-cn-beijing.aliyuncs.com/solution/demo.docx';
};
</script>

<template>
	<button @click="getFile">获取远程</button>
	<input type="file" accept=".docx" @change="previewFile" />
	<vue-office-docx :src="src" style="height: 100vh" />
</template>
