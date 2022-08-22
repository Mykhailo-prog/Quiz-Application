import axios from "axios";

export default {
  state: {
    answerList: [],
    checkFinish: false,
  },
  mutations: {
    SET_USER_ANSWER(state, payload) {
      state.answerList[payload.index] = payload.answer;
    },
    CHECK_FINISH(state, quests) {
      if (state.answerList.length === quests.length) {
        state.checkFinish = true;
      }
    },
    CLEAN_ANSWERS(state) {
      state.answerList = [];
    },
  },
  actions: {
    getUserAnswer({ commit }, payload) {
      commit("SET_USER_ANSWER", payload);
    },
    check({ commit }, quests) {
      commit("CHECK_FINISH", quests);
    },
    async finishTest({ state, commit }, payload) {
      await axios.put("https://localhost:44378/api/users/" + payload.user.id, {
        login: payload.user.login,
        password: payload.user.password,
        score: payload.user.score,
        test: payload.testId,
        userAnswers: state.answerList,
      });
    },
    cleanAnswers({ commit }) {
      commit("CLEAN_ANSWERS");
    },
  },
  getters: {},
};
