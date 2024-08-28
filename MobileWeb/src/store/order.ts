import { defineStore } from "pinia";

export const orderStore = defineStore("order", {
  //存放需要共享的数据
  state: () => {
    return {
      orderType: "takein",
      count: 1
    }
  },
  //Store 状态的计算值
  getters: {
    doubleCount: (state) => state.count * 2
  },
  //存放同步和异步方法
  actions: {
    countAdd(num: number) {
      this.count = this.count + num
    }
  },
  //开启数据持久化存储
  persist: {
    enabled: true
  }
})