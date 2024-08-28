<template>
	<span>{{ t('message.hello') }}</span>
</template>
<script setup lang="ts">
import { useI18n } from 'vue-i18n'; // 多语言

const { t } = useI18n(); // t方法取出，t('code')使用
</script>
