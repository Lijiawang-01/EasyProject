<template>
  <div>
    <van-search v-model="search" placeholder="请输入商品名称" />
    <banner-swiper :banners="banners" />
    <div v-for="cat in categories" :key="cat.id" class="category-section">
      <div class="category-header">
        <span>{{ cat.name }}</span>
        <span class="more" @click="goToCategory(cat.id)">更多 ></span>
      </div>
      <div class="product-list">
        <product-card v-for="prod in cat.hotProducts.slice(0,4)" :key="prod.id" :product="prod" />
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import BannerSwiper from '@/components/BannerSwiper.vue';
import ProductCard from '@/components/ProductCard.vue';

const router = useRouter();
const search = ref('');
const banners = ref([
  { image: 'banner1.jpg', link: '#' },
  { image: 'banner2.jpg', link: '#' }
]);
const categories = ref([
  {
    id: 1,
    name: '男装',
    hotProducts: [
      { id: 101, name: '男士T恤', price: 99, image: 't1.jpg' },
      // ...共4个
    ]
  },
  // ...更多分类
]);
function goToCategory(id: number) {
  router.push(`/classify/${id}`);
}
</script>
<style scoped>
.category-header { display: flex; justify-content: space-between; align-items: center; }
.more { color: #1989fa; cursor: pointer; font-size: 14px; }
.product-list { display: flex; gap: 10px; }
</style>