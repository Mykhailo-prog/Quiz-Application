import axios from "axios";

export default {
  state: {
    resetLinkResponse: {},
    resetPassResponse: {},
  },
  mutations: {
    SET_RESET_LINK_RESPONSE(state, res) {
      state.resetLinkResponse = res;
    },
    SET_RESET_PASSWORD_RESPONSE(state, res) {
      state.resetPassResponse = res;
    },
  },
  getters: {
    ResetLinkResponse: (state) => state.resetLinkResponse,
    ResetPassResponse: (state) => state.resetPassResponse,
  },
  actions: {
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
