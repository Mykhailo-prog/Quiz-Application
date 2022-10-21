import axios from "axios";
import { isEmptyObject } from "jquery";
//TODO: I think you need to separate Users store from authetnication store
export default {
  state: {
    userList: [],
    currUser: {},
  },
  mutations: {
    SET_USER_LIST(state, users) {
      state.userList = users;
    },
    UPDATE_CURRENT_USER(state, user) {
      state.currUser = user;
    },
    CLEAN_USER_LIST(state) {
      state.userList = [];
      state.currUser = {};
    },
  },
  getters: {
    Users: (state) => state.userList,
    CurrentUser: (state) =>
      isEmptyObject(state.currUser) ? null : state.currUser,
  },
  actions: {
    async checkRole({ state, getters }) {
      const result = await axios.post("users/checkrole", null, {
        params: { login: getters.CurrentUser.login },
      });
      return result.data;
    },
    async loadUsers({ commit }) {
      const loadedUsers = await axios.get("users");
      commit("SET_USER_LIST", loadedUsers.data);
    },
    updCurrUser({ commit }, user) {
      commit("UPDATE_CURRENT_USER", user);
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
