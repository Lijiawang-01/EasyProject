import instance from '@/http/filter';
//获取token
export const uploadFileApi = (file: any) => {
  return instance.post('/api/Login/GetToken', file);
};