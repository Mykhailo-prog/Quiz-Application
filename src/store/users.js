import axios from "axios";

export default {
  state: {
    userList: [],
    currUser: {},
    loginResponse: {},
  },
  mutations: {
    SET_USER_LIST(state, users) {
      state.userList = users;
    },
    UPDATE_CURRENT_USER(state, user) {
      state.currUser = user;
    },
    SET_LOGIN_RESPONSE(state, res) {
      state.loginResponse = res;
      state.currUser = res.user;
      localStorage.setItem("token", res.token);
    },
    CLEAN_USER_LIST(state) {
      state.userList = [];
      state.currUser = {};
      state.loginResponse = {};
    },
  },
  getters: {
    Access: (state) => state.loginResponse,
    Users: (state) => state.userList,
    CurrentUser: (state) => state.loginResponse.user,
  },
  actions: {
    async checkRole({ state }) {
      const result = await axios.post("users/checkrole", state.currUser);
      return result.data;
    },
    async loadUsers({ commit }) {
      const loadedUsers = await axios.get("users");
      commit("SET_USER_LIST", loadedUsers.data);
    },
    updCurrUser({ commit }, user) {
      commit("UPDATE_CURRENT_USER", user);
    },
    async loginUser({ commit }, form) {
      var response = await axios.post("/auth/login", form);
      commit("SET_LOGIN_RESPONSE", response.data);
    },
    async deleteUser({ commit }, payload) {
      await axios.delete("users", { params: { name: payload.name } });
    },
    async adminConfirmEmail({ commit }, payload) {
      await axios.post("users/adminconfirm", null, {
        params: { name: payload.name },
      });
    },
    async changePassword({ commit }, payload) {
      await axios.post("users/changepass", null, {
        params: { name: payload.name, password: payload.password },
      });
    },
    async resetScore({ commit }, payload) {
      await axios.post("users/resetscore", null, {
        params: { name: payload.name },
      });
    },
    cleanUsers({ commit }) {
      commit("CLEAN_USER_LIST");
    },
  },
};
