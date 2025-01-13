<template>
	<el-config-provider :locale="useLocale">
		<!-- <keep-alive> <router-view /></keep-alive> -->
		<router-view v-slot="{ Component }">
			<keep-alive>
				<component :is="Component" />
			</keep-alive>
		</router-view>
	</el-config-provider>
</template>
<script setup lang="ts">
import { ref } from 'vue';
import { ElConfigProvider } from 'element-plus';
import zh from 'element-plus/es/locale/lang/zh-cn';
import en from 'element-plus/es/locale/lang/en';
import store from '@/store';
const useLocale = store().$state.Local == 'cn' ? zh : en;
</script>

<style scoped lang="scss"></style>
