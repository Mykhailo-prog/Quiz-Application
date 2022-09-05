import axios from "axios";

export default {
  state: {
    answerList: [],
    checkFinish: false,
    procResult: null,
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
    SET_RESULT(state, result) {
      state.procResult = result;
    },
  },
  actions: {
    getUserAnswer({ commit }, payload) {
      commit("SET_USER_ANSWER", payload);
    },
    cleanAnswers({ commit }) {
      commit("CLEAN_ANSWERS");
    },
    check({ commit }, quests) {
      commit("CHECK_FINISH", quests);
    },
    async finishTest({ state, commit }, payload) {
      const res = await axios
        .put("https://localhost:44378/api/users/" + payload.user.id, {
          login: payload.user.login,
          password: payload.user.password,
          score: payload.user.score,
          test: payload.testId,
          time: payload.time,
          userAnswers: state.answerList,
        })
        .then((response) => response.data);
      commit("SET_RESULT", res);
    },
  },
  getters: {},
};
