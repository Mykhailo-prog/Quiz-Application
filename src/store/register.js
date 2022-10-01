import axios from "axios";

export default {
  state: {
    registerResponse: {},
  },
  getters: {
    RegisterResponse: (state) => state.registerResponse,
  },
  mutations: {
    SET_RESPONSE(state, res) {
      state.registerResponse = res;
    },
  },
  actions: {
    async RegisterUser({ commit }, form) {
      var response = await axios.post("auth/register", form);
      commit("SET_RESPONSE", response.data);
    },
  },
};
