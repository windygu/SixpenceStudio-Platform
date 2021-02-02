const Vue = require('vue');
const Vuex = require('vuex');

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    isLogin: false,
    auth: {
      token: '',
      userId: ''
    }
  },
  mutations: {
    changeLogin(state, data) {
      state.isLogin = data;
    },
    updateAuth(state, data) {
      const { token = '', userId = '' } = data;
      state.auth.token = token;
      state.auth.userId = userId;
    },
    clearAuth(state) {
      state.auth.token = '';
      state.auth.userId = '';
    }
  }
});
