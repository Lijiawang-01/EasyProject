<template>
  <div class="product-card" @click="goToDetail">
    <img :src="product.image" class="product-img" />
    <div class="product-info">
      <div class="product-name">{{ product.name }}</div>
      <div class="product-price">￥{{ product.price }}</div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { defineProps } from 'vue';
import { useRouter } from 'vue-router';

const props = defineProps<{
  product: {
    id: number | string;
    name: string;
    price: number | string;
    image: string;
  }
}>();

const router = useRouter();
function goToDetail() {
  router.push(`/product/${props.product.id}`);
}
</script>

<style scoped>
.product-card {
  width: 44vw;
  max-width: 180px;
  background: #fff;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0,0,0,0.04);
  overflow: hidden;
  cursor: pointer;
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-bottom: 10px;
}
.product-img {
  width: 100%;
  height: 120px;
  object-fit: cover;
}
.product-info {
  padding: 8px 6px 10px 6px;
  width: 100%;
}
.product-name {
  font-size: 15px;
  color: #333;
  margin-bottom: 4px;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}
.product-price {
  font-size: 16px;
  color: #ee0a24;
  font-weight: bold;
}
</style>
