import sysEntity from './sysEntity';
import sysMenu from './sysMenu';
import sysParamGroup from './sysParamGroup';
import userInfo from './userInfo';
import job from './job';
import sysConfig from './sysConfig';

export default [
  {
    path: '/admin',
    name: 'admin',
    children: [].concat(sysEntity, sysMenu, sysParamGroup, userInfo, job, sysConfig),
    meta: { auth: true } // 需要检验
  }
];
