import axios from "axios";
import { mapState } from "vuex";

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
    SET_NEW_TEST_ID(state, test) {
      state.newTestId = test.testId;
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
    async delTest({ dispatch }, testId) {
      await axios.delete(
        "https://localhost:44378/api/tests/" + testId.toString()
      );
      await dispatch("loadTests");
    },
    async postNewTest({ commit }, payload) {
      const newTest = await axios
        .post("https://localhost:44378/api/tests", payload.test)
        .then((response) => response.data);
      payload.quests.forEach(async (quest) => {
        const newQuest = await axios
          .post("https://localhost:44378/api/questions", {
            question: quest.question,
            correctAnswer: quest.correctAnswer,
            testId: newTest.testId,
          })
          .then((response) => response.data);
        quest.NewAnswers.forEach((ans) => {
          ans.questionId = newQuest.id;
        });
        await axios.post(
          "https://localhost:44378/api/answers",
          quest.NewAnswers
        );
      });
      await axios.post("https://localhost:44378/api/UserTestConnection", {
        testId: newTest.testId,
        userId: payload.user.id,
      });
    },
  },
  getters: {
    currTestId(state) {
      return state.currTest.testId;
    },
  },
};
