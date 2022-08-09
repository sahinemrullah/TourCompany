export const state = () => ({
  languages: [],
  countries: [],
  nationalities: [],
  genders: [],
})

export const getters = {
  getLanguages(state) {
    return state.languages
  },
  getCountries(state) {
    return state.countries
  },
  getNationalities(state) {
    return state.nationalities
  },
  getGenders(state) {
    return state.genders
  }
}

export const mutations = {
  setLanguages(state, languages)
  {
      state.languages = languages.sort((a, b) => a.name.localeCompare(b.name))
  },
  setCountries(state, countries)
  {
      state.countries = countries.sort((a, b) => a.name.localeCompare(b.name))
  },
  setNationalities(state, nationalities)
  {
      state.nationalities = nationalities.sort((a, b) => a.name.localeCompare(b.name))
  },
  setGenders(state, genders)
  {
    state.genders = genders
  }
}

export const actions = {
  async nuxtServerInit({ commit }) {
    const languages = await this.$axios.$get('/definitions/languages')
    commit('setLanguages', languages)
    const countries = await this.$axios.$get('/definitions/countries')
    commit('setCountries', countries)
    const nationalities = await this.$axios.$get('/definitions/nationalities')
    commit('setNationalities', nationalities)
    const genders = await this.$axios.$get('/definitions/genders')
    commit('setGenders', genders)
    
    return true;
  }
}