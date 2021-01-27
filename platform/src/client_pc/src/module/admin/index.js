import sysEntity from './sysEntity';
import sysMenu from './sysMenu';
import sysParamGroup from './sysParamGroup';
import userInfo from './userInfo';
import job from './job';
import sysConfig from './sysConfig';
import wechat from './wechat';
import admin from './admin';
import workplace from './workplace';
import gallery from './gallery';
import fileManage from './fileManage';
import robot from './robot';
import role from './role';

export default [
  {
    path: '/admin',
    name: 'admin',
    component: admin,
    children: [].concat(sysEntity, sysMenu, sysParamGroup, userInfo, job, sysConfig, wechat, workplace, gallery, fileManage, robot, role),
    meta: { auth: true } // 需要检验
  }
];
