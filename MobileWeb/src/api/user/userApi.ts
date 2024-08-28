import instance from '@/http/index';
//获取列表
export const getUserData = async (parms: {}) => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return instance.post('/api/Users/GetUsers', parms);
};
//个人中心修改用户昵称和密码
export const editNickNameOrPassword = async (nickName: string, password: string) => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return instance.get(`/api/Users/EditNickNameOrPassword?nickName=${nickName}&password=${password}`);
};