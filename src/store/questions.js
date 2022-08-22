import axios from "axios";

export default {
  state: {
    questList: [],
    currQuest: {},
    pugCounter: 0,
  },
  mutations: {
    SET_QUEST_LIST(state, questions) {
      state.questList = questions;
    },
    P_COUNTER(state) {
      if (state.pugCounter < state.questList.length - 1) {
        state.pugCounter += 1;
      }
    },
    M_COUNTER(state) {
      if (state.pugCounter > 0) {
        state.pugCounter -= 1;
      }
    },
    SET_COUNTER(state, count) {
      state.pugCounter = count;
    },
    CLEAN_COUNTER(state) {
      state.pugCounter = 0;
    },
  },
  getters: {
    getQuestPosition(state, pos) {
      return state.questList[pos];
    },
  },
  actions: {
    getQuestions({ commit }, questions) {
      commit("SET_QUEST_LIST", questions);
    },
    increaseCounter({ commit }) {
      commit("P_COUNTER");
    },
    decreaseCounter({ commit }) {
      commit("M_COUNTER");
    },
    getCounter({ commit }, count) {
      commit("SET_COUNTER", count);
    },
    cleanCounter({ commit }) {
      commit("CLEAN_COUNTER");
    },
  },
};
