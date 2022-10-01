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
      state.answerList.length === payload.quests.length
        ? (state.checkFinish = true)
        : (state.checkFinish = false);
    },
    CLEAN_ANSWERS(state) {
      state.answerList = [];
    },
    SET_RESULT(state, result) {
      state.procResult = result;
    },
  },
  getters: {
    Answers: (state) => state.answerList,
    CheckFinish: (state) => state.checkFinish,
  },
  actions: {
    getUserAnswer({ commit }, payload) {
      commit("SET_USER_ANSWER", payload);
    },
    cleanAnswers({ commit }) {
      commit("CLEAN_ANSWERS");
    },
  },
};
