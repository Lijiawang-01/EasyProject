import instance from '@/http/filter';
//获取列表
export const getAreaDataNew = async (params: {}) => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return instance.post('/api/Area/GetAreaList', params);
};
//添加
export const addArea = async (params: {}) => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return instance.post('/api/Area/Add', params);
};
//修改
export const editArea = async (params: {}) => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return instance.post('/api/Area/Edit', params);
};
//删除
export const delArea = async (id: string) => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return instance.get('/api/Area/Del?id=' + id);
};
//BatchDel
export const batchDelArea = async (ids: string) => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return instance.get('/api/Area/BatchDel?ids=' + ids);
};