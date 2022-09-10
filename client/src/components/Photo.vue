<template>
<v-col
    class="d-flex child-flex flex-column"
    cols="6"
>
    <h4>{{ photo.title }}</h4>
    <h5>{{photo.description}}</h5>
    <p>{{photo.createdAt.substring(0,10)}}</p>
    <div>
    <span
        class="px-2"
        v-for="tag in photo.tags"
        :key="tag.id">
        {{tag.name}}
    </span>
    </div>
    <v-img
    :src="$http.defaults.baseURL + photo.originalUrl"
    lazy-src="@/assets/1.png"
    :aspect-ratio="16/9"
    class="grey lighten-2"
    >
    <template v-slot:placeholder>
        <v-row
        class="fill-height ma-0"
        align="center"
        justify="center"
        >
        <v-progress-circular
            indeterminate
            color="grey lighten-5"
        ></v-progress-circular>
        </v-row>
    </template>
    </v-img>
</v-col>
</template>
<script>
    export default {
        name: 'photo-item',
        props: ['photo'],
        data(){
            return {

            }
        }
    }
</script>