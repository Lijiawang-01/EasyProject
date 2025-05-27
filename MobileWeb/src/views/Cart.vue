<template>
  <div class="cart-container">
    <div v-if="cartItems.length === 0" class="empty-cart">
      购物车为空
    </div>
    <div v-else>
      <div v-for="item in cartItems" :key="item.id" class="cart-item">
        <img :src="item.image" class="cart-img" />
        <div class="cart-info">
          <div class="cart-name">{{ item.name }}</div>
          <div class="cart-price">￥{{ item.price }}</div>
          <div class="cart-qty">
            <button @click="decrease(item)">-</button>
            <span>{{ item.quantity }}</span>
            <button @click="increase(item)">+</button>
          </div>
        </div>
        <button class="remove-btn" @click="remove(item.id)">删除</button>
      </div>
      <div class="cart-footer">
        <div>合计：<span class="total">￥{{ totalPrice }}</span></div>
        <button class="checkout-btn">去结算</button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue';

// 示例数据，实际项目可用pinia/vuex或本地存储
const cartItems = ref([
  { id: 1, name: '男士T恤', price: 99, image: 't1.jpg', quantity: 2 },
  { id: 2, name: '女士连衣裙', price: 199, image: 'd1.jpg', quantity: 1 },
]);

const totalPrice = computed(() => {
  return cartItems.value.reduce((sum, item) => sum + item.price * item.quantity, 0);
});

function increase(item: any) {
  item.quantity++;
}
function decrease(item: any) {
  if (item.quantity > 1) item.quantity--;
}
function remove(id: number) {
  cartItems.value = cartItems.value.filter(item => item.id !== id);
}
</script>

<style scoped>
.cart-container {
  padding: 16px;
}
.empty-cart {
  text-align: center;
  color: #888;
  margin-top: 60px;
}
.cart-item {
  display: flex;
  align-items: center;
  background: #fff;
  border-radius: 8px;
  margin-bottom: 12px;
  padding: 10px;
  box-shadow: 0 2px 8px rgba(0,0,0,0.03);
}
.cart-img {
  width: 70px;
  height: 70px;
  object-fit: cover;
  border-radius: 6px;
  margin-right: 12px;
}
.cart-info {
  flex: 1;
}
.cart-name {
  font-size: 15px;
  color: #333;
  margin-bottom: 4px;
}
.cart-price {
  color: #ee0a24;
  font-weight: bold;
  margin-bottom: 6px;
}
.cart-qty {
  display: flex;
  align-items: center;
  gap: 8px;
}
.cart-qty button {
  width: 24px;
  height: 24px;
  border: none;
  background: #f2f3f5;
  border-radius: 4px;
  font-size: 18px;
  cursor: pointer;
}
.remove-btn {
  background: none;
  border: none;
  color: #888;
  cursor: pointer;
  margin-left: 10px;
}
.cart-footer {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-top: 20px;
  padding: 10px 0;
  border-top: 1px solid #f2f3f5;
}
.total {
  color: #ee0a24;
  font-size: 18px;
  font-weight: bold;
}
.checkout-btn {
  background: #ee0a24;
  color: #fff;
  border: none;
  border-radius: 6px;
  padding: 8px 24px;
  font-size: 16px;
  cursor: pointer;
}
</style>
