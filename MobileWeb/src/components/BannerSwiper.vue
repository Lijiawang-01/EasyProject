<template>
  <van-swipe :autoplay="3000" indicator-color="white">
    <van-swipe-item v-for="(banner, index) in banners" :key="index">
      <a v-if="banner.link" :href="banner.link">
        <img :src="banner.image" class="banner-img" />
      </a>
      <img v-else :src="banner.image" class="banner-img" />
    </van-swipe-item>
  </van-swipe>
</template>

<script setup lang="ts">
import { defineProps } from 'vue';

defineProps<{
  banners: Array<{
    image: string;
    link?: string;
  }>;
}>();
</script>

<style scoped>
.banner-img {
  width: 100%;
  height: 160px;
  object-fit: cover;
  border-radius: 8px;
}
</style>
