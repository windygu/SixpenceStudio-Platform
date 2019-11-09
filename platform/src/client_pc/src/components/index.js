import login from './login'

const components = [
  { name: login.name, component: login }
]

const install = _Vue => {
  // 注册组件
  components.forEach(item => _Vue.component(item.name, item.component));
};

export default install;
