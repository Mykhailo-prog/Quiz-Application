import axios from "axios";

export default {
  state: {
    testList: [],
    currTest: {},
  },
  mutations: {
    SET_TEST_LIST(state, tests) {
      state.testList = tests;
    },
    SET_CURRENT_TEST(state, test) {
      state.currTest = test;
    },
  },
  actions: {
    async loadTests({ commit }) {
      const loadedTests = await axios.get("https://localhost:44378/api/tests");
      commit("SET_TEST_LIST", loadedTests.data);
    },
    chooseTest({ commit, state }, id) {
      const test = state.testList.find((t) => t.testId === id);
      commit("SET_CURRENT_TEST", test);
    },
  },
  getters: {
    currTestId(state) {
      return state.currTest.testId;
    },
  },
};
