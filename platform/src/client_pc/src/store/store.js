const Vue = require('vue');
const Vuex = require('vuex');

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    isLogin: false
  },
  mutations: {
    changeLogin(state, data) {
      state.isLogin = data;
    }
  }
});
