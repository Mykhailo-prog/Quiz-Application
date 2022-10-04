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
      console.log("UserUpdated!");
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
    cleanUsers({ commit }) {
      commit("CLEAN_USER_LIST");
    },
  },
};
