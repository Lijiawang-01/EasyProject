import instance from '@/http/index';
//获取列表
export const getMenuDataNew = async (params: {}) => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return instance.post('/api/Menu/GetMenus', params);
};