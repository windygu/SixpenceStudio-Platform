import sysEntity from './sysEntity';
import sysMenu from './sysMenu';
import sysParamGroup from './sysParamGroup';
import userInfo from './userInfo';
import job from './job';
import sysConfig from './sysConfig';
import wechat from './wechat';
import admin from './admin';
import workplace from './workplace';

export default [
  {
    path: '/admin',
    name: 'admin',
    component: admin,
    children: [].concat(sysEntity, sysMenu, sysParamGroup, userInfo, job, sysConfig, wechat, workplace),
    meta: { auth: true } // 需要检验
  }
];
