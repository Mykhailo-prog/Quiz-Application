export default {
  state: {
    questListLength: 0,
    pugCounter: 0,
  },
  mutations: {
    CHANGE_COUNTER(state, param) {
      if (param === "+") {
        if (state.pugCounter < state.questListLength - 1) {
          state.pugCounter += 1;
        }
      } else if (param === "-") {
        if (state.pugCounter > 0) {
          state.pugCounter -= 1;
        }
      } else {
        state.pugCounter = param;
      }
    },
    SET_QUEST_LIST_LENGTH(state, questions) {
      state.questListLength = questions.length;
    },
    CLEAN_COUNTER(state) {
      state.pugCounter = 0;
    },
  },
  getters: {
    Questions: (state, getters) => getters.CurrentTest.questions,
    PuginationCounter: (state) => state.pugCounter,
  },
  actions: {
    changeCounter({ commit }, param) {
      commit("CHANGE_COUNTER", param);
    },
    cleanCounter({ commit }) {
      commit("CLEAN_COUNTER");
    },
  },
};
