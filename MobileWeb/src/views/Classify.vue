<template>
  <div>
    <div v-for="cat in categories" :key="cat.id" @click="goToCategory(cat.id)">
      {{ cat.name }}
    </div>
  </div>
</template>
<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
const router = useRouter();
const categories = ref([
  { id: 1, name: '男装' },
  { id: 2, name: '女装' },
  // ...
]);
function goToCategory(id: number) {
  router.push(`/classify/${id}`);
}
</script>