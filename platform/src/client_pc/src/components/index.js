import spLogin from './spLogin'

const components = [
  { name: spLogin.name, component: spLogin }
]

const install = _Vue => {
  // 注册组件
  components.forEach(item => _Vue.component(item.name, item.component));
};

export default install;
