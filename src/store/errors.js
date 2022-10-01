import axios from "axios";

export default {
  state: {
    errorResponse: {},
  },
  mutations: {
    SET_RESPONSE(state, res) {
      state.errorResponse = res;
    },
    RESET_ERRORS(state) {
      state.errorResponse = {};
    },
  },
  getters: {
    errorResponse: (state) => state.errorResponse,
  },
  actions: {
    resetErrors({ commit }) {
      commit("RESET_ERRORS");
    },
  },
};
