import axios from "axios";

export default {
  state: {
    userList: [],
    currUser: {},
  },
  mutations: {
    SET_USER_LIST(state, users) {
      state.userList = users;
    },
    SET_CURRENT_USER(state, user) {
      state.currUser = state.userList.find((u) => u.login === user.login);
    },
    CLEAN_USER_LIST(state) {
      state.userList = [];
      state.currUser = {};
    },
  },
  getters: {
    currUser(state) {
      return state.currUser;
    },
  },
  actions: {
    async loadUsers({ commit }) {
      const loadedUsers = await axios.get("https://localhost:44378/api/users");
      commit("SET_USER_LIST", loadedUsers.data);
    },
    async checkUser({ commit, state, dispatch }, userLogin) {
      const User = state.userList.find((u) => u.login === userLogin);
      if (User === undefined) {
        const newUser = await axios.post("https://localhost:44378/api/users", {
          login: userLogin,
          password: "",
        });
        await dispatch("loadUsers");
        commit("SET_CURRENT_USER", newUser.data);
      } else {
        commit("SET_CURRENT_USER", User);
      }
    },
    cleanUsers({ commit }) {
      commit("CLEAN_USER_LIST");
    },
  },
};
