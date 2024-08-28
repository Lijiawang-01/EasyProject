
import axios from "axios"
import { showSuccessToast, showFailToast } from 'vant';

const instance = axios.create({
  // baseURL: "/api",
  headers: {
    // 'content-type': 'application/json',
    "content-type": "application/json;charset=UTF-8"
  },
  timeout: 20000
})

//请求拦截器
instance.interceptors.request.use(
  config => {
    if (localStorage.getItem('token')) {
      config.headers.Authorization = localStorage.getItem('token')
    }

    return config
  },
  err => {
    return Promise.reject(err)
  }
)


//http 拦截器
instance.interceptors.response.use(
  (response) => {
    //拦截请求，统一相应
    if (response.data.isSuccess) {
      return response.data.result;
    } else {
      showFailToast(response.data.msg);
      return response.data.result;
    }
  },
  //error也可以处理
  (error) => {
    if (error.response) {
      switch (error.response.status) {
        case 401:
          showFailToast('资源没有访问权限！');
          break;
        case 404:
          showFailToast('接口不存在，请检查接口地址是否正确！');
          break;
        case 500:
          showFailToast('内部服务器错误，请联系系统管理员！');
          break;
        default:
          return Promise.reject(error.response.data); // 返回接口返回的错误信息
      }
    } else {
      showFailToast('遇到跨域错误，请设置代理或者修改后端允许跨域访问！');
    }
  }
);
export default instance;
