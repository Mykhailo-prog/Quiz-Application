import axios from "axios";
import store from ".";

export default {
  state: {
    registerResponse: {},
    resetLinkResponse: {},
    resetPassResponse: {},
    loginResponse: {},
  },
  getters: {
    RegisterResponse: (state) => state.registerResponse,
    ResetLinkResponse: (state) => state.resetLinkResponse,
    ResetPassResponse: (state) => state.resetPassResponse,
    LoginResponse: (state) => state.loginResponse,
  },
  mutations: {
    SET_RESPONSE(state, res) {
      state.registerResponse = res;
    },
    SET_RESET_LINK_RESPONSE(state, res) {
      state.resetLinkResponse = res;
    },
    SET_RESET_PASSWORD_RESPONSE(state, res) {
      state.resetPassResponse = res;
    },
    SET_LOGIN_RESPONSE(state, res) {
      state.loginResponse = res;
      localStorage.setItem("token", res.token);
    },
  },
  actions: {
    async loginUser({ commit }, form) {
      var response = await axios.post("/auth/login", form);
      commit("SET_LOGIN_RESPONSE", response.data);
      store.commit("UPDATE_CURRENT_USER", response.data.object);
    },
    async RegisterUser({ commit }, form) {
      var response = await axios.post("auth/register", form);
      commit("SET_RESPONSE", response.data);
    },
    async sendResetLink({ commit }, email) {
      const responce = await axios.post("auth/forgetpassword?email=" + email);
      commit("SET_RESET_LINK_RESPONSE", responce.data);
    },
    async resetPassword({ commit }, form) {
      const response = await axios.post("auth/resetpassword", form);
      commit("SET_RESET_PASSWORD_RESPONSE", response.data);
    },
  },
};
/**/
