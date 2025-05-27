<template>
  <div>
    <h2>{{ categoryName }}</h2>
    <div class="product-list">
      <product-card v-for="prod in products" :key="prod.id" :product="prod" />
    </div>
  </div>
</template>
<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import ProductCard from '@/components/ProductCard.vue';

const route = useRoute();
const categoryName = ref('');
const products = ref([]);
onMounted(() => {
  // 根据 route.params.id 获取分类和商品
  // categoryName.value = ...
  // products.value = ...
});
</script>