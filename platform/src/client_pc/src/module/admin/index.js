import sysEntity from './sysEntity';
import sysMenu from './sysMenu';
import sysParamGroup from './sysParamGroup';
import userInfo from './userInfo';

export default [
  {
    path: '/admin',
    name: 'admin',
    component: () => import('./admin'),
    children: [].concat(sysEntity, sysMenu, sysParamGroup, userInfo),
    meta: { auth: true } // 需要检验
  }
];
